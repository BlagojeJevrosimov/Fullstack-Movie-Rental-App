import { NgModule } from "@angular/core";
import { RouterLink, RouterOutlet } from "@angular/router";
import { AppComponent } from "./app.component";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule, provideAnimations } from "@angular/platform-browser/animations";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { CustomersModule } from "./customers/customers.module";
import { MoviesModule } from "./movies/movies.module";
import { MatButtonModule } from "@angular/material/button";
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from "@angular/material/menu";
import { MsalModule, MsalRedirectComponent, MsalInterceptor} from "@azure/msal-angular";
import { PublicClientApplication } from "@azure/msal-browser";
import { AppRoutingModule } from "./app-routing-module";
import { msalConfig } from "./auth-config";
import { MSALInterceptorConfigFactory } from "./msal-interceptor-config";
import { AdminRoleGuard } from "./guards/RoleGuard";




@NgModule({
  imports: [
    RouterOutlet,
    AppRoutingModule,
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CustomersModule,
    MoviesModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule,
    RouterLink,
    MsalModule.forRoot(new PublicClientApplication(msalConfig),null!,MSALInterceptorConfigFactory()),
  ],
  providers: [provideAnimations(), {
    provide: HTTP_INTERCEPTORS,
    useClass: MsalInterceptor,
    multi: true,
  },
  AdminRoleGuard
 ],
  declarations: [AppComponent],
  exports: [AppComponent],
  bootstrap: [AppComponent, MsalRedirectComponent]
})

export class AppModule { }