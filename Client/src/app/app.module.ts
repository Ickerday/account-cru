import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { environment } from '../environments/environment';
import { AppComponent } from './app.component';
import { AccountListComponent } from './account-list/account-list.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { AccountDetailComponent } from './account-detail/account-detail.component';
import { AccountsClient } from './client/client';

const appRoutes: Routes = [
  { path: 'accounts', component: AccountListComponent },
  { path: 'accounts/:id', component: AccountDetailComponent },
  { path: '**', component: PageNotFoundComponent },
  { path: '', redirectTo: '/accounts', pathMatch: 'full' },

];

@NgModule({
  declarations: [
    AppComponent,
    AccountListComponent,
    AccountDetailComponent,
    PageNotFoundComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: !environment.production }
    )
  ],
  providers: [
    { provide: AccountsClient, useClass: AccountsClient }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
