import { Routes, Route } from 'react-router-dom'
import ProductListPage from './pages/Products/ProductListPage'
import ThemeProvider from './theme'

function App() {
  return (
    <ThemeProvider>
    <div>
      <Routes>
        <Route path="/" element={<ProductListPage />} />
      </Routes>
    </div>
    </ThemeProvider>
  )
}

export default App