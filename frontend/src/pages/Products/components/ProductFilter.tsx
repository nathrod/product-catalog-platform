import { Input, Select } from "antd";

export default function ProductFilters() {
    return (
        <>
            <Input
                placeholder="Code"
                style={{ width: 220 }}
            />

            <Input
                placeholder="Name"
                style={{ width: 220 }}
            />

            <Input
                placeholder="Price"
                style={{ width: 140 }}
            />

            <Select
                placeholder="Category"
                style={{ width: 160 }}
                options={[
                    { value: 1, label: "Electronics" },
                    { value: 2, label: "Clothing" },
                    { value: 3, label: "Home" },
                ]}
            />

            <Select
                placeholder="Available"
                style={{ width: 150 }}
                options={[
                    { value: true, label: "Available" },
                    { value: false, label: "Out of Stock" },
                ]}
            />
        </>
    );
}