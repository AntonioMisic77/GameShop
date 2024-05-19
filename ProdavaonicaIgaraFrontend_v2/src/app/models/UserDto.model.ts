export class UserDto {
    id: number;
    firstName: string;
    lastName: string;
    username: string | null;
    iban: string | null;
    email: string;
  
    constructor(data: any = {}) {
      this.id = data.id || 0;
      this.firstName = data.firstName || '';
      this.lastName = data.lastName || '';
      this.username = data.username || null;
      this.iban = data.iban || null;
      this.email = data.email || '';
    }
  }
  