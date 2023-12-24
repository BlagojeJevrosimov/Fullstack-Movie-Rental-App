import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CustomersClient, LicensingTypes, Movie, PurchaseMovieCommand } from '../../api/api-reference';
import { UserService } from '../../user.service';

@Component({
  selector: 'app-movie-card',
  templateUrl: './movie-card.component.html',
  styleUrl: './movie-card.component.css',
})
export class MovieCardComponent {
  @Input() movieInfo!: Movie;
  @Output() deleteMovie: EventEmitter<string> = new EventEmitter<string>
  licensingTypeEnum: typeof LicensingTypes = LicensingTypes;
  constructor(private readonly customerClient: CustomersClient, private userService: UserService) { }

  selectBuyMovie(movieId: string | undefined) {
        this.customerClient.purchaseMovie(new PurchaseMovieCommand({
          movieId : movieId,
          customerId : this.userService.getUser().id
        })).subscribe(_ => console.log(_))
  }
  selectDeleteMovie(movieId: string) {
    this.deleteMovie.emit(movieId);
  }
}
