import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from 'src/app/salo/osoba';
import { Observable } from 'rxjs';


@Injectable()
export class AuthHttpService{

        base_url = "http://localhost:52295"

        constructor(private http: HttpClient){

        }

        logIn(username: string, password: string){

            let data = `username=${username}&password=${password}&grant_type=password`;
            let httpOptions = {
                headers:{
                    "Content-type": "application/x-www-form-urlencoded"
                }
            }
            this.http.post<any>(this.base_url + "/oauth/token",data, httpOptions )
            .subscribe(data => {
                localStorage.jwt = data.access_token;
            });
        }

        registration(data:User)
        {
             return this.http.post<any>(this.base_url + "/api/Account/Register", data).subscribe();
            
        }

        GetCenaKarte(tip: string): Observable<any>{
            return this.http.get<any>(this.base_url + "/api/PriceOfTickets/GetKarta/" + tip);
        }
        GetKupiKartu(tipKarte: string, tipKorisnika: string, user : string): Observable<any>{
           
            return this.http.get<any>(this.base_url + "/api/PriceOfTickets/GetKartaKupi2/" + tipKarte + "/" + tipKorisnika + "/" + user);
        }
}