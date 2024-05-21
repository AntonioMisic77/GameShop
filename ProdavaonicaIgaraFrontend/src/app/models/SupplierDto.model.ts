export class SupplierDto {
    id: number;
    name: string;
    address: string | null;
    iban: string | null;
    email: string;
  
    constructor(data: any = {}) {
      this.id = data.id || 0;
      this.name = data.name || '';
      this.address = data.address || null;
      this.iban = data.iban || null;
      this.email = data.email || '';
    }
  }
  