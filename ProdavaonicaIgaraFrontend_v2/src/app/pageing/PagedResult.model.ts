export class PagedResult<T>{

    constructor(
        public items:Array<T>,
        public pageNumber: number,
        public pageSize: number,
        public totalCount: number
    ){}
}