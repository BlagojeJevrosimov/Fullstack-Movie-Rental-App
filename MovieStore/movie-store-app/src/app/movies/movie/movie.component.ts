import { Component, OnInit } from '@angular/core';
import { CreateMovieCommand, LicensingTypes, Movie, MoviesClient, UpdateMovieCommand } from '../../api/api-reference';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrl: './movie.component.css'
})
export class MovieComponent implements OnInit {
  movieForm = new FormGroup({
    name: new FormControl("", [ Validators.minLength(3),Validators.required]),
    licensingType: new FormControl<LicensingTypes>(0,Validators.required),
    dateOfRelease: new FormControl<Date>(new Date(),Validators.required)
  })
  movie!: Movie 
  movieId!: string

  constructor(private builder: FormBuilder, private readonly movieClient: MoviesClient, private router: Router, private route: ActivatedRoute, private snackbar: MatSnackBar) { }
  ngOnInit(): void {
    this.route.url.subscribe( _ => {
      this.movieId = this.route.snapshot.paramMap.get('id')!;
      if (this.movieId !== null) {
        this.movieClient.getMovieById(this.movieId).subscribe(response => {
          this.movie = response
          this.movieForm.controls.name.setValue(response.name! )
          this.movieForm.controls.licensingType.setValue(response.licensingType!) 
          this.movieForm.controls.dateOfRelease.setValue(response.dateOfRelease!) 
        });

      } 
    });

  }

  submitMovie() {
    if (this.movieId !== null) {
      this.movieClient.updateMovie(new UpdateMovieCommand({
        id: this.movieId,
        name: this.movieForm.controls.name.dirty ? this.movieForm.value.name! : this.movie.name,
        dateOfRelease: this.movieForm.controls.dateOfRelease.dirty ? this.movieForm.value.dateOfRelease! : this.movie.dateOfRelease,
      })).subscribe({
        next: _ => {this.router.navigateByUrl("/movies");this.snackbar.open('Successful')},
        error: err => this.snackbar.open('Problem editing the movie')
      })
    }
    else if(this.movieForm.valid){
      this.movieClient.createMovie(new CreateMovieCommand({
        name: this.movieForm.value.name ? this.movieForm.value.name : undefined,
        licensingType: this.movieForm.value.licensingType!.toString() === "0" ? LicensingTypes.TwoDay : LicensingTypes.LifeLong,
        dateOfRelease: this.movieForm.value.dateOfRelease ? this.movieForm.value.dateOfRelease : undefined,
      })).subscribe({
        next: _ => {this.router.navigateByUrl('/movies'); this.snackbar.open('Movie Added')},
        error: () => this.snackbar.open('Problem while adding movie')
      } )
    }
    this.snackbar.open('Fill in all the required fields')
  }
  
}
