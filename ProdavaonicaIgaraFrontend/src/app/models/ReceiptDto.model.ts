import { ReceiptItemDto } from './ReceiptItemDto.model';
import { CompanyDto } from './CompanyDto.model';
import { UserDto } from './UserDto.model';

export class ReceiptDto {
  id: number;
  cashierId: number;
  companyId: number;
  paymentMethod: string;
  date: Date;
  receiptItems: ReceiptItemDto[] | null;
  company: CompanyDto | null;
  cashier: UserDto | null;

  constructor(data: any = {}) {
    this.id = data.id || 0;
    this.cashierId = data.cashierId || 0;
    this.companyId = data.companyId || 0;
    this.paymentMethod = data.paymentMethod || '';
    this.date = data.date ? new Date(data.date) : new Date();
    this.receiptItems = data.receiptItems || null;
    this.company = data.company || null;
    this.cashier = data.cashier || null;
  }
}
