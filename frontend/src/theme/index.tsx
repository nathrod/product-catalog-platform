import React from 'react';
import { ConfigProvider } from 'antd';


const ThemeProvider = ({ children }: {children: React.ReactNode}) => {
    const primaryColorCode = "#102F15" 
    //AEA781
    return (
        <ConfigProvider
            theme={{
                token: {
                    colorPrimary: primaryColorCode,
                },
                components: {
                    Button: {
                        controlHeight: 45,
                    },
                    Table: {
                        borderRadius: 0,
                        headerBorderRadius: 0,
                    },
                },
            }}
        >
            {children}
        </ConfigProvider>
    )
}

export default ThemeProvider;