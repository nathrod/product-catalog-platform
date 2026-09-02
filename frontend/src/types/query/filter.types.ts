export type FilterFieldType =
    | 'text'
    | 'number'
    | 'select'
    | 'boolean';

export interface FilterOption {
    label: string;
    value: string | number | boolean;
}

export interface FilterField {
    name: string;
    label: string;
    type: FilterFieldType;
    options?: FilterOption[];
}