import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BoardComponent } from './components/board/board.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { GamePlayComponent } from './components/game-play/game-play.component';
import { HttpClientModule } from '@angular/common/http';
import { NgxSpinnerModule } from 'ngx-spinner';
import {NgxAutoScrollModule} from 'ngx-auto-scroll';

@NgModule({
  declarations: [
    AppComponent,
    BoardComponent,
    GamePlayComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    CommonModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    FormsModule,
    NgxSpinnerModule,
    NgxAutoScrollModule
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
