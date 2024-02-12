/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ['./**/*.{razor,html}', '../PortfolioSiteHost.Client/**/*.{razor,html}'],
    theme: {
        extend: {
            fontFamily: {
                display: 'Montserrat, Jura, sans-serif',
                logo: 'Stick-No-Bills, sans-serif',
            },
            colors: {
                accent: '#7100DB',
            }
        },
    },
    plugins: [],
}