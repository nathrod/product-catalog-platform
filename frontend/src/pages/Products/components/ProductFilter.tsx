import { Button, Input, InputNumber, Select } from "antd";
import { useState } from "react";
import { ProductCategoryValues } from "@/constants/enum";

export type ProductFilterValues = {
    code?: string;
    name?: string;
    price?: number;
    category?: number;
    isActive?: boolean;
};

type ProductFiltersProps = {
    onFilter: (values: ProductFilterValues) => void;
}

export default function ProductFilters({
    onFilter,
}: ProductFiltersProps) {
    const [filters, setFilters] = useState<ProductFilterValues>({});

    const handleClear = () => {
        setFilters({});
        onFilter({});
    };

    return (
        <div className="flex flex-wrap items-center gap-3">
            <Input
                placeholder="Code"
                style={{ width: 220 }}
                value={filters.code}
                onChange={(e) => 
                    setFilters((current) => ({
                        ...current,
                        code: e.target.value,
                    }))
                }
            />

            <Input
                placeholder="Name"
                style={{ width: 220 }}
                value={filters.name}
                onChange={(e) => 
                    setFilters((current) => ({
                        ...current,
                        name: e.target.value,
                    }))
                }
            />

            <InputNumber
                placeholder="Price"
                className="w-35"
                min={0}
                value={filters.price}
                onChange={(value) => 
                    setFilters((current) => ({
                        ...current,
                        price: value ?? undefined,
                    }))
                }
            />

            <Select
                placeholder="Category"
                className="w-45"
                allowClear
                value={filters.category}
                onChange={(value) =>
                    setFilters((current) => ({
                        ...current,
                        category: value,
                    }))
                }
                options={[
                    { value: ProductCategoryValues.Electronics, label: "Electronics" },
                    { value: ProductCategoryValues.ClothingAndApparel, label: "Clothing" },
                    { value: ProductCategoryValues.HomeAndGarden, label: "Home and Garden" },
                    { value: ProductCategoryValues.BooksAndMedia, label: "Books" },
                    { value: ProductCategoryValues.HealthAndBeauty, label: "Health and Beauty" },
                    { value: ProductCategoryValues.ToysAndGames, label: "Toys and Games" },
                    { value: ProductCategoryValues.SportsAndOutdoors, label: "Sports and Outdoors" },
                    { value: ProductCategoryValues.Automotive, label: "Automotive" },
                    { value: ProductCategoryValues.GroceryAndFood, label: "Grocery and Food" },
                    { value: ProductCategoryValues.PetSupplies, label: "Pet Supplies" },
                    { value: ProductCategoryValues.NoCategorized, label: "No Categorized" },
                ]}
            />

            <Select
                placeholder="Available"
                className="w-42.5"
                allowClear
                value={filters.isActive}
                onChange={(value) => 
                    setFilters((current) => ({
                        ...current,
                        isActive: value,
                    }))
                }
                options={[
                    { value: true, label: "Available" },
                    { value: false, label: "Out of Stock" },
                ]}
            />

            <Button type="primary" onClick={() => onFilter(filters)}>
                Filter
            </Button>

            <Button onClick={handleClear}>Clear</Button>
        </div>
    );
}