import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TradeService } from '../../core/services/trade.service';
import { Position } from '../../core/models/position.model';
import { Trade } from '../../core/models/trade.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-trades',
  standalone: false,
  templateUrl: './trades.component.html',
  styleUrls: ['./trades.component.css']
})
export class TradesComponent implements OnInit {
  tradeForm!: FormGroup;
  positions: Position = {};
  loading = false;

  constructor(
    private fb: FormBuilder,
    private tradeService: TradeService,
    private snackbar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.tradeForm = this.fb.group({
      tradeId: [null, [Validators.required, Validators.min(1)]],
      version: [1, [Validators.required, Validators.min(1)]],
      securityCode: ['', [Validators.required, Validators.maxLength(10)]],
      quantity: [0, [Validators.required, Validators.min(1)]],
      action: ['Insert', Validators.required],
      type: ['Buy', Validators.required],
    });

    this.loadPositions();
  }

  onSubmit(): void {
    if (this.tradeForm.invalid) {
      this.snackbar.open('Please correct the form errors.', 'Close', { duration: 3000 });
      return;
    }

    this.loading = true;
    this.tradeService.addTrade(this.tradeForm.value).subscribe({
      next: () => {
        this.snackbar.open('Trade submitted successfully!', 'Close', { duration: 3000 });
        this.tradeForm.reset({ action: 'Insert', type: 'Buy', version: 1, quantity: 0 });
        this.loadPositions();
      },
      error: (err) => {
        console.log(err);
        this.snackbar.open(err.message || 'Failed to submit trade.', 'Close', { duration: 4000 });
      },
      complete: () => {
        this.loading = false;
        this.loadPositions();
      }
    });
  }

  loadPositions(): void {
    this.tradeService.getPositions().subscribe({
      next: (res) => (this.positions = res),
      error: () => this.snackbar.open('Failed to load positions.', 'Close', { duration: 3000 })
    });
  }
}
