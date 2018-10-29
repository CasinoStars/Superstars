const webpack = require("webpack");
const path = require("path");
const ExtractTextWebpackPlugin = require("extract-text-webpack-plugin");
const autoprefixer = require('autoprefixer');

const wwwroot = "../../wwwroot";

let config = {
    entry: "./src/main.js",
    output: {
        path: path.resolve(wwwroot, "./dist"),
        publicPath: process.env.NODE_ENV === "production" ? "/dist/" : "http://localhost:8080/dist/",
        filename: "./bundle.js"
    },
    module: {
        rules: [
            {
                test: /\.vue$/,
                loader: "vue-loader",
                options: {
                    postcss: [autoprefixer()],
                    loaders: {
                        css: ExtractTextWebpackPlugin.extract({
                            use: 'css-loader',
                            fallback: 'vue-style-loader'
                        }),
                        scss: ExtractTextWebpackPlugin.extract({
                            use: 'css-loader!sass-loader',
                            fallback: 'vue-style-loader'
                        })
                    }
                }
            },
            {
                test: /\.js$/,
                exclude: /node_modules/,
                loader: "babel-loader"
            },
            {
                test: /\.(png|jpg|gif|svg)$/,
                loader: "file-loader",
                query: {
                    name: "[name].[ext]?[hash]"
                }
            },
            {
                test: /\.(woff2?|eot|ttf|otf)(\?.*)?$/,
                loader: "file-loader",
                query: {
                    name: "[name].[ext]?[hash]"
                }
            }
        ]
    },
    devServer: {
        historyApiFallback: true,
        noInfo: true,
        headers: {
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Methods": "GET"
        }
    },

    devtool:
        process.env.NODE_ENV === "production" ? "#source-map" : "#eval-source-map",

    plugins: [
        new webpack.DefinePlugin({
            "process.env": {
                NODE_ENV: JSON.stringify(process.env.NODE_ENV || "development")
            }
        }),
        new ExtractTextWebpackPlugin("style.css")
    ]
}

module.exports = config;

if (process.env.NODE_ENV === "production") {
    module.exports.plugins = (module.exports.plugins || []).concat([
        new webpack.optimize.UglifyJsPlugin({
            sourceMap: true,
            compress: {
                warnings: false
            }
        }),
        new webpack.LoaderOptionsPlugin({
            minimize: true
        })
    ]);
}