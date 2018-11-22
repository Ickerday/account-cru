import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountsClient } from '../client/client';

@Component({
  selector: 'app-account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.scss']
})
export class AccountListComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private client: AccountsClient
  ) { }

  ngOnInit() {
  }

}
