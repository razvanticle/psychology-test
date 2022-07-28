import React, {useEffect, useState} from 'react';
import {Field} from 'react-final-form'
import Wizard from '../../components/wizard'

const Error = ({name}) => (
    <Field
        name={name}
        subscribe={{touched: true, error: true}}
        render={({meta: {touched, error}}) =>
            touched && error ? <span>{error}</span> : null
        }
    />
)

const Test = ({match, history}) => {
    const [loading, setLoading] = useState(true);
    const [template, setTemplateData] = useState(null);
    const [error, setError] = useState("");
    
    const [resultLoading, setResultLoading] = useState(false);

    useEffect(() => {
        const {id} = match.params;
        fetchTestTemplate(id);
    }, [match]);

    const validateQuestion = (value) => {
        return (value ? undefined : 'Please select an answer');
    }

    const onSubmit = async (values) => {
        await postData('https://localhost:7137/tests', values)
            .then((data) => {
                history.push(`/tests/${data.id}`);
            });
    }

    const postData = async (url = '', data = {}) => {
        try{
            setResultLoading(true);
            
            const response = await fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });
            return response.json();    
        }
        catch (error){
            setError(error.message || "Error when caomputing the test result");
        }
        finally {
            setResultLoading(false);
        }
    }

    const fetchTestTemplate = async (id) => {
        try {
            const template = await fetch(`https://localhost:7137/templates/${id}`);
            const templateData = await template.json();

            setTemplateData(templateData);         
        }
        catch (error){
            setError(error.message || "Error when loading the test template");
        }
        finally {
            setLoading(false);
        }        
    }
   
    return (
        <div>
            {loading && <p>Loading...</p>}
            {error && <p>{error}</p>}
            {template && <div>
                <h1>{template.title}</h1>
                <p>{template.description}</p>
                <Wizard
                    initialValues={{
                        testTemplateId: template.id,
                        answers: template.questions.map(question => {
                            return {questionId: question.id}
                        })
                    }}
                    onSubmit={onSubmit}
                >
                    {template.questions.map((question, index) =>
                        <Wizard.Page key={`question-${question.id}`}>
                            <h4>{question.title}</h4>
                            {question.answers.map(answer =>
                                <p key={`answer-${answer.id}`}>
                                    <Field name={`answers[${index}].answerId`} component="input" type="radio"
                                           value={`${answer.id}`} validate={validateQuestion}/> {answer.content}
                                </p>
                            )}
                            <Error name={`answers[${index}].answerId`}/>

                        </Wizard.Page>
                    )}
                </Wizard>
            </div>}
            {resultLoading && <p>Loading test results...</p>}
        </div>
    );
}

export default Test;