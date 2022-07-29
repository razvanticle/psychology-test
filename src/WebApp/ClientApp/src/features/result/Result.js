import React, {useEffect, useState} from 'react';
import {apiClient} from '../../common/api/apiClient';

const Test = ({match}) => {
    const [result, setResult] = useState(null);
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        const {id} = match.params;
        getResult(id);
    }, [match]);

    const getResult = (id) => {
        setLoading(true);

        apiClient.getById('tests', id)
            .then(setResult)
            .catch(setError)
            .finally(() => setLoading(false));
    }
    
    return (
        <div>
            {loading && <p>Loading...</p>}
            {error && <p>{error}</p>}
            {result && !loading && <div>
                <h1>{result.testName}</h1>
                <hr/>
                <h2>You are more of an {result.result}</h2>
                <p>{result.description}</p>                
            </div>}
        </div>
    );
}

export default Test;