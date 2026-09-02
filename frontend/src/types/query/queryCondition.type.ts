import type { Filter } from "./filter.type";
import type { Sort } from "./sort.type";

export type QueryCondition = {
    pageSize: number;
    pageIndex: number;
    filters?: Filter[];
    sorts?: Sort[];
};