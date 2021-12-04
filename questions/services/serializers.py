from rest_framework import serializers
from questions.models import (
    Question,
    QuestionsAnswers
)


class QuestionAnswersSerializer(serializers.ModelSerializer):
    class Meta:
        model = QuestionsAnswers
        fields = ('name', 'is_correct')


class QuestionSerializer(serializers.ModelSerializer):
    answers = QuestionAnswersSerializer(read_only=True, many=True)
    category = serializers.SlugRelatedField(slug_field='name', read_only=True)

    class Meta:
        model = Question
        fields = ('short_name', 'wording', 'category', 'answers')
