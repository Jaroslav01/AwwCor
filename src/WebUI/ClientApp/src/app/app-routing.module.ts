import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DevEnvGuard } from './nav-menu/dev-env.guard';
import {ProductComponent} from './product/product.component';

export const routes: Routes = [

  // { path: 'counter', component: CounterComponent },
  // { path: 'fetch-data', component: FetchDataComponent },
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'product', component: ProductComponent },
  // { path: 'todo', component: TodoComponent, canActivate: [AuthorizeGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
