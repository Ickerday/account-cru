import { Component, Inject, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

import { IAccount } from '../../models/account';
import { AccountsClient } from '../client/client';
import { switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-account-detail',
  templateUrl: './account-detail.component.html',
  styleUrls: ['./account-detail.component.scss']
})
export class AccountDetailComponent implements OnInit {
  account$: Observable<IAccount>;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private client: AccountsClient
  ) {

  }

  ngOnInit() {
    this.account$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
        this.client.get(+params.get('id'))
      ));
  }

  gotoAccounts() {
    this.router.navigate(['/accounts']);
  }
}
