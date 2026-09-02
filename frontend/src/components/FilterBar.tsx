import { DownOutlined, FilterOutlined, UpOutlined } from '@ant-design/icons';
import { Button, Space } from 'antd';
import { useState } from 'react';

type FilterBarProps = {
    children: React.ReactNode;
};

export default function FilterBar({ children }: FilterBarProps) {
    const [collapsed, setCollapsed] = useState(false);

    return (
        <div className="rounded-t-lg border border-b-0 border-gray-200 bg-white">
            {/* Cabeçalho */}
            <div className="flex items-center justify-between px-4 py-2">
                <div className="flex items-center gap-2 text-gray-600">
                    <FilterOutlined />
                    <span className="text-sm font-medium">Filters</span>
                </div>

                <Button
                    type="text"
                    size="small"
                    icon={
                        collapsed
                            ? <DownOutlined />
                            : <UpOutlined />
                    }
                    onClick={() => setCollapsed(!collapsed)}
                />
            </div>

            {/* Campos */}
            {!collapsed && (
                <div className="border-t border-gray-200 px-4 py-3">
                    <Space
                        wrap
                        size={[10, 10]}
                        className="w-full"
                    >
                        {children}
                    </Space>
                </div>
            )}
        </div>
    );
}