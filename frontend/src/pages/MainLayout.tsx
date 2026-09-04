import React, {useState } from 'react';
import { Breadcrumb, Layout, Menu, theme } from 'antd';
import { 
    HomeOutlined, 
    ShoppingOutlined, 
    ShoppingCartOutlined,
} from '@ant-design/icons';
import { Outlet } from 'react-router-dom'

const { Header, Sider, Content, Footer } = Layout;

const MainLayout: React.FC = () => {
    const [collapsed, setCollapsed] = useState(false);
    const {
        token: { colorBgContainer, borderRadiusLG },
    } = theme.useToken();

    const currentYear = new Date().getFullYear();

    return (
        <Layout style={{ height: '100vh', overflow: 'hidden' }}>
            <Sider collapsible collapsed={collapsed} onCollapse={(value) => setCollapsed(value)}>
                <div className="demo-logo-vertical" />

                <Menu
                theme="dark"
                mode="inline"
                defaultSelectedKeys={['1']}
                items={[
                    {
                        key: 'home',
                        icon: <HomeOutlined />,
                        label: 'Home',
                    },
                    {
                        key: 'products',
                        icon: <ShoppingOutlined />,
                        label: 'Products',
                    },
                    {
                        key: 'sales',
                        icon: <ShoppingCartOutlined />,
                        label: 'Sales',
                    },
                ]}
                />
            </Sider>

            <Layout style={{ minWidth: 0, minHeight: 0 }}>
                <Header style={{ padding: 0, background: colorBgContainer }} />
                
                <Content 
                    style={{
                        margin: '0 16px',
                        minHeight: 0,
                        overflow: 'hidden',
                        display: 'flex',
                        flexDirection: 'column',
                    }}
                >
                    <Breadcrumb style={{ margin: '16px 0' }} items={[{ title: 'Home' }, { title: 'Products' }]} />
                    <div
                        style={{
                        padding: 24,
                        flex: 1,
                        minHeight: 0,
                        overflow: 'hidden',
                        background: colorBgContainer,
                        borderRadius: borderRadiusLG,
                        }}
                    >
                        <Outlet />
                    </div>
                </Content>
                <Footer style={{ textAlign: 'center' }}>
                    Products Company ©{currentYear} Created by NRod
                </Footer>
            </Layout>
        </Layout>
    );
};

export default MainLayout;