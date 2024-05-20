import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-root',
  standalone:true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'ProdavaonicaIgaraFrontend';

  constructor(private router: Router) {}

  ngOnInit() {
    this.navigateToReceipts();
  }

  navigateToReceipts() {
    this.router.navigate(['/receipts']);
  }
}
