import { Component } from '@angular/core';
import { Router, RouterModule, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ProdavaonicaIgaraFrontend_v2';

  constructor(private router: Router) {}

  ngOnInit() {
    this.navigateToReceipts();
  }

  navigateToReceipts() {
    this.router.navigate(['/receipts']);
  }
  navigateToArticles(){
    this.router.navigate(['/articles'])
  }
}
