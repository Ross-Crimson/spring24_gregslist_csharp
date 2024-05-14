export const dev = window.location.origin.includes('localhost')

// NOTE don't forget to change your baseURL if using the dotnet template
export const baseURL = dev ? 'https://localhost:7045' : ''
export const useSockets = false

// TODO change these variables out to your own auth after cloning!
export const domain = 'dev-5xpsq3obgjbpnv0x.us.auth0.com'
export const clientId = 'GJz5ztEkQ63sCaSj5dRbZZF9fB6p2iWK'
export const audience = 'https://ross_p_codeworks.com'