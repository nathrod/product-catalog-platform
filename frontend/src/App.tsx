import { Routes, Route } from 'react-router-dom'
import ProductListPage from '@/pages/Products/ProductListPage'
import MainLayout from '@/pages/MainLayout'
import ThemeProvider from '@/theme'

function App() {
  return (
      <ThemeProvider>
        <div className="h-full">
        <Routes>
          <Route element={<MainLayout />}>
            <Route 
              path='/'
              element={<ProductListPage />}
            />
          <Route 
            path="/products" 
            element={<ProductListPage />} 
          />
          </Route>
        </Routes>
        </div>
      </ThemeProvider>
  )
}

export default App