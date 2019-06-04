import { Component, OnInit } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormArray } from '@angular/forms';
import { AuthHttpService } from 'src/app/services/http/auth.service';
import { User } from 'src/app/salo/osoba';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit  {

  registacijaForm = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    username: ['', Validators.required],
    password: ['', Validators.required],
    confirmPassword: ['', Validators.required],
    email: ['', Validators.required],
    date: ['', Validators.required]
  });
  
  constructor(private http: AuthHttpService, private fb: FormBuilder, private router: Router) { }
  
    ngOnInit() {
    }
  
    register(){
      let regModel: User = this.registacijaForm.value;
      this.http.registration(regModel);
      //form.reset();
    }

}
