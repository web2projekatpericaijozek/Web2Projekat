import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes} from '@angular/router'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SaloComponent } from './salo/salo.component';
import { HttpService } from 'src/app/services/http.service';
import { HttpClientModule} from '@angular/common/http'
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from 'src/app/interceptors/token.interceptor';
import { HomeComponent } from 'src/app/home/home.component';
import { LoginComponent } from 'src/app/login/login.component';
import { FormsModule} from '@angular/forms'
import { AuthHttpService } from 'src/app/services/http/auth.service';
import { RegistrationComponent } from './registration/registration.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NavigacijaComponent } from './navigacija/navigacija.component';

const routes : Routes = [
  {path:"home", component: HomeComponent},
  {path:"login", component: LoginComponent},
  {path:"registration", component: RegistrationComponent},
  {path: "", component: HomeComponent, pathMatch: "full"},
  {path: "**", redirectTo: "home"}
]

@NgModule({
  declarations: [
    AppComponent,
    SaloComponent,
    LoginComponent,
    RegistrationComponent,
    HomeComponent,
    RegistrationComponent,
    NavigacijaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes)
  ],
  providers: [HttpService, {provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true},AuthHttpService],
  bootstrap: [AppComponent]
})
export class AppModule { }
