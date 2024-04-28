import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
  server: {
    port: 80,
    host: true,
    strictPort: true,
  },
  plugins: [react()],
})
