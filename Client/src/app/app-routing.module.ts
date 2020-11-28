import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GamePlayComponent } from './components/game-play/game-play.component';
import { BoardComponent } from './components/board/board.component';

const routes: Routes = [
  { path: '', component: BoardComponent},
  { path: 'game-play/:name', component: GamePlayComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
