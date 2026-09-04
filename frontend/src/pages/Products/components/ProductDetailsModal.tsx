import { Descriptions, Image, Modal, Tag, Typography } from 'antd'
import { PictureOutlined } from '@ant-design/icons'

import type { Product } from '@/types/products/product.type'

import {
    ProductCategoryLabels,
    ProductPriorityLabels,
    ProductPriorityValues,
} from '@/constants/enum'

const { Title, Text, Paragraph } = Typography

type ProductDetailsModalProps = {
    open: boolean
    onClose: () => void
    product?: Product | null
}

export default function ProductDetailsModal({
    open,
    onClose,
    product,
}: ProductDetailsModalProps) {
    if (!product) {
        return null
    }

    const priorityColor = {
        [ProductPriorityValues.Low]: 'green',
        [ProductPriorityValues.Medium]: 'gold',
        [ProductPriorityValues.High]: 'red',
    }

    return (
        <Modal
            title="Product Details"
            open={open}
            onCancel={onClose}
            footer={null}
        >
        <div className="grid grid-cols-[280px_1fr] gap-8 py-4">

        
            <div className='flex h-70 items-center justify-center overflow-hidden rounded-lg border border-gray-200 bg-gray-50'>
                {product.imageURL ? (
                    <Image
                        src={product.imageURL}
                        alt={product.name}
                        className='max-h-70 object-contain'
                    />
                ) : (
                    <div className='flex flex-col items-center gap-2 text-gray-400'> 
                        <PictureOutlined className='text-4xl' />
                        <span>No image available</span>
                    </div>
                )}
            </div>
        </div>

        <div>
            <div className='mb-6'>
                <Title level={3} className="mb-1!">
                    {product.name}
                </Title>

                <Text type="secondary">
                    {product.code}
                </Text>
            </div>

            <Title level={2} className='mb-6!'>
                {product.price.toLocaleString('pt-BR', {
                    style: 'currency',
                    currency: 'BRL',
                })}
            </Title>

            <Descriptions
                column={1}
                size='small'
                colon={false}
            >
                <Descriptions.Item label="Category">
                    {ProductCategoryLabels[product.category]}
                </Descriptions.Item>

                <Descriptions.Item label="Priority">
                    <Tag color={priorityColor[product.priority]}>
                        {ProductPriorityLabels[product.priority]}
                    </Tag>
                </Descriptions.Item>

                <Descriptions.Item label="Status">
                    <Tag color={product.isActive ? 'green' : 'red'}>
                        {product.isActive
                            ? 'Available'
                            : 'Out of Stock'
                        }
                    </Tag>
                </Descriptions.Item>
            </Descriptions>
        </div>

        <div className='mt-4 border-t border-gray-200 pt-5'>
            <Title level={5}>
                Description
            </Title>

            <Paragraph type={product.description ? undefined : 'secondary'}>
                {product.description || 'No description available.'}
            </Paragraph>
        </div>
        </Modal>
    )
}