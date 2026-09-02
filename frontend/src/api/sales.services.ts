// GET /sales
// POST /sales
// POST /sales/import-csv/{productId}

import { api } from '../config/axios'
import type { PagedResult } from '../types/pageListResult.type';
import type { CreateSales, Sales } from '../types/sales.type';

export default class SalesService {
    static async getAll(): Promise<PagedResult<Sales>> {
        const { data } = await api.get('/sales');
        return data;
    }

    static async createSale(payload: CreateSales): Promise<Sales> {
        const { data } = await api.post('/sales', payload);
        return data;
    }

    //backend IFormFile file
    static async createSales(file: File, productId: string): Promise<number> {
        const formData = new FormData();
        formData.append('file', file);

        const { data } = await api.post(`/sales/import-csv/${productId}`, formData);
        return data;
    }
}