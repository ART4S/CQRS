const path = require('path');
const webpack = require('webpack');
const BrowserSyncPlugin = require('browser-sync-webpack-plugin');

module.exports = {
  entry: './src/index.tsx',
  mode: 'development',
  module: {
    rules: [
        {
            test: /\.(t|j)sx?$/,
            use: { loader: 'awesome-typescript-loader' } 
        },
        {
            enforce: 'pre',
            test: /\.js$/,
            loader: 'source-map-loader' 
        },
    ],
  },
  resolve: { extensions: ['.ts', '.tsx', '.js', 'jsx'] },
  output: {
    path: path.resolve(__dirname, 'dist/'),
    publicPath: '/dist/',
    filename: 'bundle.js',
  },
  devServer: {
    contentBase: path.join(__dirname, 'public/'),
    port: 5000,
    publicPath: 'http://localhost:5000/dist/',
    hotOnly: true,
  },
  plugins: [
    new webpack.HotModuleReplacementPlugin(),
    new BrowserSyncPlugin({
        host: 'localhost',
        port: 8080,
        proxy: 'http://localhost:5000/',
    }),
  ],
};