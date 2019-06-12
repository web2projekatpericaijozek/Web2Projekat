import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from 'src/app/salo/osoba';
import { Observable } from 'rxjs';



@Injectable()
export class AuthHttpService{

        base_url = "http://localhost:52295"

        user: string

        constructor(private http: HttpClient){

        }

        logIn(username: string, password: string){

            let data = `username=${username}&password=${password}&grant_type=password`;
            let httpOptions = {
                headers:{
                    "Content-type": "application/x-www-form-urlencoded"
                }
            }
           // return this.http.get<any>(this.base_url + "/api/Account/GetTipKorisnika/" + username)
            this.http.post<any>(this.base_url + "/oauth/token",data, httpOptions )
            .subscribe(data => {
            localStorage.jwt = data.access_token;
            let jwtData = localStorage.jwt.split('.')[1]
            let decodedJwtJsonData = window.atob(jwtData)
            let decodedJwtData = JSON.parse(decodedJwtJsonData)

  
            let role = decodedJwtData.role
            this.user = decodedJwtData.unique_name;
            
            });
            //return this.http.get<any>(this.base_url + "/api/Account/GetTipKorisnika/" + username);
        }

        public isAuthenticated(): boolean {
            if(localStorage.jwt != "undefined")
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }
        
        logIn2(username: string, password: string){

             this.http.get<any>(this.base_url + "/api/Account/GetTipKorisnika/" + username).subscribe();
        }

       proveri(id : string):Observable<any>{
            return this.http.get<any>(this.base_url + "/api/PriceOfTickets/GetProveriKartu/" + id);
        }
       

        registration(data:User)
        {
             return this.http.post<any>(this.base_url + "/api/Account/Register", data).subscribe();
            
        }

        edit(data:User,user: string)
        {
             return this.http.post<any>(this.base_url + "/api/Account/Edit/" + user, data).subscribe();
            
        }

        GetCenaKarte(tip: string): Observable<any>{
            return this.http.get<any>(this.base_url + "/api/PriceOfTickets/GetKarta/" + tip);
        }
        GetCenaKarte2(tip: string, tipKorisnika : string): Observable<any>{
            return this.http.get<any>(this.base_url + "/api/PriceOfTickets/GetKartaCena/" + tip + "/" + tipKorisnika);
        }
        GetPromeniCenu(tip:string,cena: number ): Observable<any>{
            return this.http.get<any>(this.base_url + "/api/PriceOfTickets/GetPromeniCenu/" + tip + "/" + cena);
        }

        GetPromeniLiniju(stara:number,nova: number ): Observable<any>{
            return this.http.get<any>(this.base_url + "/api/Lines/GetPromeniLiniju/" + stara + "/" + nova);
        }
        GetDodajCenovnik(tip:string,cena: number ): Observable<any>{
            return this.http.get<any>(this.base_url + "/api/PriceOfTickets/GetDodaj/" + tip + "/" + cena);
        }
        GetObrisiCenovnik(id:number ): Observable<any>{
            return this.http.get<any>(this.base_url + "/api/PriceOfTickets/GetObrisi/" + id );
        }
        GetKupiKartu(tipKarte: string, tipKorisnika: string, user : string): Observable<any>{
           
            return this.http.get<any>(this.base_url + "/api/PriceOfTickets/GetKartaKupi2/" + tipKarte + "/" + tipKorisnika + "/" + user);
        }
        GetAllLines() : Observable<any>{
            return Observable.create((observer) => {
                this.http.get<any>(this.base_url + "/api/Lines/GetLinije").subscribe(data =>{
                    observer.next(data);
                    observer.complete();
                }) 
            });
        }
        GetTipKorisnika(user : string): Observable<any>{
           
            return this.http.get<any>(this.base_url + "/api/Account/GetTipKorisnika/" + user);
        }
        GetRolaKorisnika(user : string): Observable<any>{
           
            return this.http.get<any>(this.base_url + "/api/Account/GetRolaKorisnika/" + user);
        }

        GetKorisnik(user : string): Observable<any>{
           
            return this.http.get<any>(this.base_url + "/api/Account/GetKorisnik/" + user);
        }
}