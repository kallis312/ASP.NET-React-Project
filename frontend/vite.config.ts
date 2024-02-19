import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'

// https://vitejs.dev/config/
export default defineConfig({
  server: {
    host: true,
    proxy: {
      '/api': 'http://localhost:5053'
    }
  },
  build: {
    outDir: '../wwwroot'
  },
  plugins: [react()],
})
