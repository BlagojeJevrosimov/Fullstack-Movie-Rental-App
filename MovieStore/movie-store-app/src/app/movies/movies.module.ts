import { NgModule } from "@angular/core";
import { MatButtonModule } from "@angular/material/button";
import { MovieCardComponent } from "./movie-card/movie-card.component";
import { ListMoviesComponent } from "./list-movies/list-movies.component";
import { MatCardModule } from "@angular/material/card";
import { MatSnackBarModule } from "@angular/material/snack-bar";
import { CommonModule } from "@angular/common";
import { MoviesRoutingModule } from "./movies-routing.module";
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import { ReactiveFormsModule } from "@angular/forms";
import { MovieComponent } from "./movie/movie.component";
import { MatSelectModule } from "@angular/material/select";
import { MatPaginatorModule } from "@angular/material/paginator";


@NgModule({
    imports: [
        MatButtonModule,
        MatCardModule,
        MatSnackBarModule,
        CommonModule,
        MoviesRoutingModule,
        MatSelectModule,
        MatInputModule,
        MatFormFieldModule,
        MatDatepickerModule,
        MatNativeDateModule,
        ReactiveFormsModule,
        MatPaginatorModule
    ],
    declarations: [MovieCardComponent, ListMoviesComponent, MovieComponent]
})

export class MoviesModule { }