import { Component, Inject } from '@angular/core';
import { FormsModule, ReactiveFormsModule, FormControl, FormGroup } from '@angular/forms';
import { Http } from '@angular/http';
import { Router } from '@angular/router';


@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent {
    public error: string;
    baseUrl: string;
    constructor(private http: Http, @Inject('BASE_URL') baseUrl: string,private router: Router) {
        this.baseUrl = baseUrl;
    }

    signupfrm = new FormGroup({
        username: new FormControl(),
        password: new FormControl()
    });

    
    onSubmit() {
        this.http.get(this.baseUrl + 'api/Login/Login?username=' + this.signupfrm.value.username + "&password=" + this.signupfrm.value.password).subscribe(result => {
            if (result.json() != null)
                this.router.navigateByUrl('/home');
            this.error = "Some information are not right!";
        }, error => console.error(error));
    }
}
