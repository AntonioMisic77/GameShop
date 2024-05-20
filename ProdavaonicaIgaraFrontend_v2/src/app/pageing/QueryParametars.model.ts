export class QueryParametars{

    constructor(
        public startIndex:number,
        public pageNumber: number,
        public filterText: string,
        public pageSize: number
    ){}
}