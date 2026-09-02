import type { FilterField } from '../../../types/query/filter.types';

export const productFilters: FilterField[] = [
    {
        name: 'code',
        label: 'Code',
        type: 'text',
    },
    {
        name: 'name',
        label: 'Name',
        type: 'text',
    },
    {
        name: 'price',
        label: 'Price',
        type: 'number',
    },
    {
        name: 'category',
        label: 'Category',
        type: 'select',
        //Create an ENUM for this options
        options: [
            { label: 'Electronics', value: 1 },
            { label: 'Clothing', value: 2 },
            { label: 'Books', value: 4 },
        ],
    },
    {
        name: 'isActive',
        label: 'Available',
        type: 'boolean',
        options: [
            { label: 'Available', value: true },
            { label: 'Out of Stock', value: false },
        ],
    },
];