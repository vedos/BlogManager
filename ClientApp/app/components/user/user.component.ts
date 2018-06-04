import { Component, OnInit, OnDestroy, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Http } from '@angular/http';

@Component({
    selector: 'user',
    templateUrl: './user.component.html'
})

export class UserComponent implements OnInit, OnDestroy {
    id: number = 1;
    user: User;
    blogs: Blog[];
    private sub: any;
    baseUrl: string;
    constructor(private route: ActivatedRoute, private http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            this.id = +params['id'];

            // In a real app: dispatch action to load the details here.
            this.http.get(this.baseUrl + 'api/Users/GetUserDetails?id=' + this.id).subscribe(result => {
                this.user = result.json() as User;
                this.blogs = result.json()["blogs"] as Blog[];
            }, error => console.error(error));
        });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}

interface User {
    FirstName: string;
    LastName: string;
    Age: string;
    Email: string;
}

interface Blog {
    Title: string;
    Summary: string;
    PublishingDateTime: string;
}