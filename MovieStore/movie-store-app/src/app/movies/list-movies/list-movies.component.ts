import { Component, OnInit, ViewChild } from '@angular/core';
import { DeleteMovieByIdCommand, LicensingTypes, Movie, MoviesClient } from '../../api/api-reference';
import { NumberInput } from '@angular/cdk/coercion';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-list-movies',
  templateUrl: './list-movies.component.html',
  styleUrl: './list-movies.component.css',
})

export class ListMoviesComponent implements OnInit {

  movies: Movie[] = [];
  paginatedMovies: Movie[] = []
  movieCount: number = 0
  //@ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;

  constructor(private readonly moviesClient: MoviesClient,) { }
  ngOnInit() {
    this.moviesClient.getMovieCount().subscribe({
      next: response=> this.movieCount = response
    })

    this.moviesClient.getMoviePagination(0,5).subscribe({
      next: response => {
        this.paginatedMovies = response;
      },
      error: error => console.log(error)
    })
  }

  readonly deleteMovie = (movieId: string) =>
    this.moviesClient
      .deleteMovieById(new DeleteMovieByIdCommand({ id: movieId }))
      .subscribe(_ => this.paginatedMovies = this.paginatedMovies.filter(movie => movie.id !== movieId));

  onPageChange(event: PageEvent): void {
    this.moviesClient.getMoviePagination(event.pageIndex,event.pageSize).subscribe({
      next: response => this.paginatedMovies = response,
      error: error => console.log(error)
    })
  }
}


