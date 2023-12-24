import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ListMoviesComponent } from "./list-movies/list-movies.component";
import { MovieComponent } from "./movie/movie.component";

const routes: Routes = [
    {
      path: '',
      component: ListMoviesComponent,
    },
    {
      path:'add',
      component:MovieComponent
    },
    {
      path:'edit/:id',
      component:MovieComponent
    }
    ];

@NgModule({
    imports:[RouterModule.forChild(routes)],
    exports:[RouterModule]
})
export class MoviesRoutingModule{}