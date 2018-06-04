import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { FormControl } from '@angular/forms';
import { AppSettings } from "read-appsettings-json";
import { Router } from '@angular/router';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})

export class HomeComponent {
    public users: User[];
    queryField: FormControl = new FormControl();
    p: number = 1;
    pageSize: number = AppSettings.Current().AppSettings.ItemsPerPage;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string, private router: Router) {
        http.get(baseUrl + 'api/Users/GetUsersList').subscribe(result => {
            this.users = result.json() as User[];
        }, error => console.error(error));

        this.queryField.valueChanges
            .subscribe(queryField => http.get(baseUrl + 'api/Users/SearchUsers?query=' + queryField).subscribe(result => {
                this.users = result.json() as User[];
            }, error => console.error(error)));
    }
}

interface User {
    Name: string;
    Age: string;
    Email: string;
    NumberOfBlogs: string;
}
