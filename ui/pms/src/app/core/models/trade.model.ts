export interface Trade {
  tradeId: number;
  version: number;
  securityCode: string;
  quantity: number;
  action: 'Insert' | 'Update' | 'Cancel';
  type: 'Buy' | 'Sell';
}
