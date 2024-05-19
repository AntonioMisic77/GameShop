import { SupplierDto } from './SupplierDto.model';

export class ArticleDto {
  id: number;
  supplierId: number;
  name: string;
  description: string;
  price: number;
  stockQuantity: number;
  supplier: SupplierDto | null;

  constructor(data: any = {}) {
    this.id = data.id || 0;
    this.supplierId = data.supplierId || 0;
    this.name = data.name || '';
    this.description = data.description || '';
    this.price = data.price || 0;
    this.stockQuantity = data.stockQuantity || 0;
    this.supplier = data.supplier || null;
  }
}
