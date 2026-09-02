export type Sales = {
    id: string;
    productId: string;
    salesDate: Date;
    quantitySold: number;
    totalSalesAmount: number;
}

export type CreateSales = Omit<Sales, 'id'>;