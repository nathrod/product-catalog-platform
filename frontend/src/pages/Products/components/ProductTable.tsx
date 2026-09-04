import { useEffect, useState } from "react";
import type { Product } from "@/types/products/product.type";
import type { QueryCondition } from "@/types/query/queryCondition.type";
import ProductService from "@/api/products.service";
import { Button, message, Popconfirm, Table } from "antd";
import FilterBar from "@/components/FilterBar";
import ProductFilters, {type ProductFilterValues} from "./ProductFilter";
import type { Filter } from "@/types/query/filter.types";
import { ProductCategoryLabels } from "@/constants/enum";

import {
    EditOutlined,
    EyeOutlined,
    DeleteOutlined,
} from '@ant-design/icons';

type ProductTableProps = {
    refreshKey: number
    onRefresh: () => void
    onEdit: (product: Product) => void
    onVisualize: (id: string) => void
};

export default function ProductTable({
    refreshKey,
    onRefresh,
    onEdit,
    onVisualize,
}: ProductTableProps) {

    const [products, setProducts] = useState<Product[]>([]);
    const [loading, setLoading] = useState(false);
    const [total, setTotal] = useState(0);

    const [query, setQuery] = useState<QueryCondition>({
        pageSize: 10,
        pageIndex: 1,
    });
    
    useEffect(() => {
        const loadProducts = async () => {
            try{
                setLoading(true);
                const result = await ProductService.getAll(query);
                setProducts(result.items);
                setTotal(result.total);
            } catch (error) {
                console.error('Error loading products:', error);
            } finally {
                setLoading(false);
            }
        };
        loadProducts();
    }, [query, refreshKey]);

    const handleDelete = async (id: string) => {
        try {
            await ProductService.delete([id]);

            message.success('Product deleted successfully');

            onRefresh();
        } catch (error) {
            console.error('Error deleting product:', error);
            message.error('Failed to delete product');
        }
    };
    
    const columns = [
        {
            key: '1',
            title: 'Code',
            dataIndex: 'code',
            sorter: true,
        },
        {
            key: '2',
            title: 'Name',
            dataIndex: 'name', 
            sorter: true,
        },
        {
            key: '3',
            title: 'Price',
            dataIndex: 'price', 
            sorter: true,
        },
        {
            key: '4',
            title: 'Category',
            dataIndex: 'category',
            sorter: true,
            render: (category: Product["category"]) => (
                <span>{ProductCategoryLabels[category]}</span>
            ),
        },
        {
            key: '5',
            title: 'Priority',
            dataIndex: 'priority',
            sorter: true, 
        },
        {
            key: '6',
            title: 'Available',
            dataIndex: 'isActive',
            render:(isActive: boolean)=>{
                return <p>{isActive?'Available':'Out of Stock'}</p>
            }
        },
        {
            key: 'actions',
            title: 'Actions',
            width: 150,
            align: 'center' as const,
            render: (_: unknown, product: Product) => (
                <div className="flex justify-center gap-2">
                    <Button
                        shape="circle"
                        icon={<EditOutlined />}
                        onClick={() => onEdit(product)}
                    />

                    <Button
                        shape="circle"
                        icon={<EyeOutlined />}
                        onClick={() => onVisualize(product.id)}
                    />

                    <Popconfirm
                        title="Delete product"
                        description="Are you sure you want to delete this product?"
                        okText="Yes"
                        cancelText="No"
                        onConfirm={() => handleDelete(product.id)}
                    >
                        <Button
                            danger
                            icon={<DeleteOutlined />}
                        />
                    </Popconfirm>
                </div>
            ),
        },
    ]

    const handleFilter = (values: ProductFilterValues) => {
        const filters: Filter[] = Object.entries(values)
            .filter(([_, value]) =>
                value !== undefined &&
                value !== null &&
                value !== ""
            )
            .map(([fieldName, fieldValue]) => ({
                fieldName,
                fieldValue: String(fieldValue),
            }));

        setQuery((current) => ({
            ...current,
            pageIndex: 1,
            filters,
        }));
    };

    return (
        <div className="flex h-full min-h-0 flex-col overflow-hidden rounded-lg border border-gray-200 bg-white">

            <FilterBar>
                <ProductFilters onFilter={handleFilter}/>
            </FilterBar>

            <div className="min-h-0 flex-1 border-t border-gray-200">

            <Table<Product>
                rowKey="id"
                loading={loading}
                dataSource={products}
                columns={columns}
                size="middle"
                scroll={{
                    y: 'calc(100vh - 500px)',
                }}
                pagination={{
                    current: query.pageIndex,
                    pageSize: query.pageSize,
                    total: total,
                    showTotal: (total) => `Total ${total} items`,
                }}
                onChange={(pagination, _filters, sorter)=>{
                    const sorterInfo = Array.isArray(sorter) ? sorter[0] : sorter;
                    
                    setQuery({
                        ...query,
                        pageSize: pagination.pageSize ?? 10,
                        pageIndex: pagination.current ?? 1,
                        sorts: sorterInfo?.order
                            ?[{
                                fieldName: sorterInfo.field as string,
                                descending: sorterInfo.order === "descend",
                            }]
                            : [],
                    });
                }}
            />
            </div>
        </div>
    )
}