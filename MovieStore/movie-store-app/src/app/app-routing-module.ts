import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { AdminRoleGuard } from './guards/RoleGuard';

export const routes: Routes = [
  {
    path: 'customers',
    loadChildren: () =>
      import('./customers/customers.module').then((m) => m.CustomersModule),
    canActivate: [AdminRoleGuard],
  },
  { path: '', redirectTo: '/movies', pathMatch: 'full' },
  {
    path: 'movies',
    loadChildren: () =>
      import('./movies/movies.module').then((m) => m.MoviesModule),
  },
  { path: '**', redirectTo: '/movies' },
];

const isIframe = window !== window.parent && !window.opener;

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
