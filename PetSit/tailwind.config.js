/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './Views/**/*.cshtml', // If you're using Razor Views, this points to your .cshtml files.
        './Scripts/**/*.js',   // JavaScript files in the Scripts folder.
    ],
    theme: {
        extend: {},
    },
    plugins: [],
};
