from rest_framework import serializers
from learning.models import Test, LearningResults, UserTestAnswers, TestQuestion
from questions.services.serializers import QuestionListSerializer
from django.contrib.auth.models import User


class UserSerializer(serializers.ModelSerializer):
    class Meta:
        model = User
        fields = ('username', 'email')


class TestSerializer(serializers.ModelSerializer):
    questions = QuestionListSerializer(read_only=True, many=True)

    class Meta:
        model = Test
        fields = ('name', )


class TestQuestionSerializer(serializers.ModelSerializer):
    question = serializers.SlugRelatedField(slug_field='short_name', read_only=True)
    category = serializers.SlugRelatedField(slug_field='category.name', read_only=True)

    class Meta:
        model = TestQuestion
        fields = ('question', 'category', )


class TestAnswersSerializer(serializers.ModelSerializer):
    test_question = TestQuestionSerializer(read_only=True)
    answer_correctly = serializers.BooleanField(read_only=True)

    class Meta:
        model = UserTestAnswers
        fields = ('test_question', 'answer_correctly')


class TestResultsSerializer(serializers.ModelSerializer):
    test = serializers.SlugRelatedField(slug_field='name', read_only=True)
    user = UserSerializer(read_only=True)
    test_answers = TestAnswersSerializer(many=True, read_only=True)

    class Meta:
        model = LearningResults
        fields = ('test', 'user', 'test_answers', )
