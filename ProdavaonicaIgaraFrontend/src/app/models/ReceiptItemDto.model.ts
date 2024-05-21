import { ArticleDto } from './ArticleDto.model';

export class ReceiptItemDto {
  id: number;
  receiptId: number;
  articleId: number;
  quantity: number;
  article: ArticleDto | null;

  constructor(data: any = {}) {
    this.id = data.id || 0;
    this.receiptId = data.receiptId || 0;
    this.articleId = data.articleId || 0;
    this.quantity = data.quantity || 0;
    this.article = data.article || null;
  }
}
