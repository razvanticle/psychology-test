import React, {useEffect, useState} from 'react';
import {Field} from 'react-final-form'
import Wizard from '../../components/wizard'
import {apiClient} from '../../common/api/apiClient';

const Error = ({name}) => (
    <Field
        name={name}
        subscribe={{touched: true, error: true}}
        render={({meta: {touched, error}}) =>
            touched && error ? <span className="form-error">{error}</span> : null
        }
    />
)

const Test = ({match, history}) => {
    const [template, setTemplate] = useState(null);
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        const {id} = match.params;
        getTemplate(id);
    }, [match]);

    const getTemplate = (id) => {
        setLoading(true);

        apiClient.getById('templates', id)
            .then(setTemplate)
            .catch(setError)
            .finally(() => setLoading(false));
    }

    const getResult = (data) => {
        setLoading(true);

        return apiClient.post('tests', data)
            .catch(setError)
            .finally(() => setLoading(false));
    }

    const validateQuestion = (value) => {
        return (value ? undefined : 'Please select an answer');
    }

    const onSubmit = async (values) => {
        const result = await getResult(values);
        history.push(`/results/${result.id}`);
    }

    return (
        <div className="container">
            {loading && <p>Loading...</p>}
            {error && <p>{error}</p>}
            {template && !loading && <div>
                <h1>{template.title}</h1>
                <p>{template.description}</p>
                <hr />
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
                            <div>
                            <Error name={`answers[${index}].answerId`}/>
                            </div>
                        </Wizard.Page>
                    )}
                </Wizard>
            </div>}
        </div>
    );
}

export default Test;