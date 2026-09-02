export type PagedResult<T> = {
    total: number;
    pageSize: number;
    pageIndex: number;
    items: T[];
};