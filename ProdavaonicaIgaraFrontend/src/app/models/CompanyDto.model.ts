export class CompanyDto {
    id: number;
    name: string;
    address: string | null;
    email: string;
    phone: string | null;
  
    constructor(data: any = {}) {
      this.id = data.id || 0;
      this.name = data.name || '';
      this.address = data.address || null;
      this.email = data.email || '';
      this.phone = data.phone || null;
    }
  }
  