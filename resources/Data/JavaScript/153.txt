module.exports = {
    query: query,
};

const Pool = require("pg").Pool;

function query(queryString, cbFunc) {
    const pool = new Pool({
        user: "postgres",
        host: "localhost",
        database: "logrocket_oauth2",
        password: "postgres",
        port: 5432,
    });

    pool.query(queryString, (error, results) => {
        cbFunc(setResponse(error, results));
    });
}

function setResponse(error, results) {
    return {
        error: error,
        results: results ? results : null,
    };
}